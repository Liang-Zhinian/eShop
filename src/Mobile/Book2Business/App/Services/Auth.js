import { setItem, getItem, mergeItem } from './StorageService'

const authorize = (email, password) => {
    return new Promise((resolve, reject) => {
        var scopes = 'role openid identity sites'
        var data = `grant_type=password&scope=${scopes}&username=${email}&password=${password}&client_id=ro.client&client_secret=secret`;

        var xhr = new XMLHttpRequest();
        xhr.withCredentials = true;

        xhr.addEventListener('readystatechange', function () {
            if (this.readyState !== 4) {
                return;
            }
            if (this.status === 200) {
                try {
                    const responseJson = JSON.parse(this.response)
                    resolve(responseJson)
                } catch (reason) {
                    reject(reason);
                }
            } else {
                reject(Error(this.response));
            }
        });

        xhr.open('POST', 'http://localhost:55105/connect/token');
        xhr.setRequestHeader('cache-control', 'no-cache');
        xhr.setRequestHeader('content-type', 'application/x-www-form-urlencoded');
        xhr.setRequestHeader('accept', 'application/json');

        xhr.send(data);
    });
}

const refresh = () => { }

const revoke = () => { }

export default (function () {
    signInWithEmailAndPassword = (email, password) => {
        return new Promise((resolve, reject) => {
            authorize(email, password)
                .then(async (res) => {
                    await setItem('identity', { auth_time: new Date(), email, password, ...res })
                    const userData = await getUserData(email)
                    
                    this.currentUser = { uid: email, ...userData }

                    resolve({
                        user: {
                            uid: email,
                            email: email,
                            emailVerified: true
                        }
                    })
                })

        })
    }

    getUserData = async (username) => {
        const identity = await getItem('identity')
        // const url = `http://localhost:55137/api/v1/usermanagement/users/with-user-name/${username}`
        const url = `http://localhost:55143/api/v1/staffs?username=${username}`
        const authorization = `${identity.token_type} ${identity.access_token}`
        
        return fetch(url, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                authorization
            }
        })
            .then(res => res.json())
        // .then(resJson => mergeItem('identity', { user: resJson }))
    }

    this.currentUser = null
    this.auth = () => {
        return {
            signInWithEmailAndPassword: signInWithEmailAndPassword.bind(this),
            currentUser: this.currentUser
        }
    }

    this.getUserData = getUserData

    return this
})()