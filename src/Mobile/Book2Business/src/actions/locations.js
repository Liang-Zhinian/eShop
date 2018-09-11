import ErrorMessages from '../constants/errors';
import statusMessage from './status';
import { Firebase, FirebaseRef } from '../lib/firebase';
import site from '../constants/site';

export function getLocation(siteId, locationId) {
    const { api } = site;

    return dispatch => new Promise(async (resolve, reject) => {

        await statusMessage(dispatch, 'loading', true);

        // Go to Firebase
        return fetch(`${api}locations/ofsiteid/${siteId}/locationid/${locationId}`)
            .then(res=>res.json())
          .then((json) => {
            await statusMessage(dispatch, 'loading', false);
            return resolve(dispatch({
                type: 'LOCATION',
                data: json,
              }));
          }).catch(reject);
        
    }).catch(async (err) => {
        statusMessage(dispatch, 'loading', false);
        throw err.message;
    });
}

/**
  * Update Profile
  */
export function updateLocation(formData) {
    const {
        latitude,
        longitude,
        streetAddress,
        city,
        stateProvince,
        postalCode,
        country,
        primaryTelephone,
        secondaryTelephone,
        contactName,
    } = formData;

    return dispatch => new Promise(async (resolve, reject) => {

        await statusMessage(dispatch, 'loading', true);

        
    }).catch(async (err) => {
        await statusMessage(dispatch, 'loading', false);
        throw err.message;
    });
}

