import ErrorMessages from '../constants/errors';
import statusMessage from './status';
import site from '../constants/site';

/**
  * Set an Error Message
  */
export function setError(message) {
    return dispatch => new Promise(resolve => resolve(dispatch({
        type: 'SERVICE_CATEGORIES_ERROR',
        data: message,
    })));
}

/**
  * Get ServiceCategories
  */
export function getServiceCategories(siteId, pageSize, pageIndex) {
    const { api } = site;

    return dispatch => new Promise(async (resolve, reject) => {

        await statusMessage(dispatch, 'loading', true);
        const url = `${api}Catalog/sites/${siteId}?pageSize=${pageSize}&pageIndex=${pageIndex}`;
        console.log(url);
        return fetch(url)
            .then(res => res.json())
            .then((json) => {
                console.log(json)
                return resolve(dispatch({
                    type: 'SERVICE_CATEGORIES_REPLACE',
                    data: json,
                }));
            }).catch(reject);

    }).catch(async (err) => {
        await statusMessage(dispatch, 'loading', false);
        throw err.message;
    });
}
