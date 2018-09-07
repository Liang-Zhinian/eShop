import ErrorMessages from '../constants/errors';
import statusMessage from './status';
import { Firebase, FirebaseRef } from '../lib/firebase';

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

