import ErrorMessages from '../Constants/errors'
import statusMessage from './status'
import {CatalogApiUrl} from '../Constants/api'

/**
  * Set an Error Message
  */
export function setError (message) {
  return dispatch => new Promise(resolve => resolve(dispatch({
    type: 'SERVICE_CATEGORIES_ERROR',
    data: message
  })))
}

/**
  * Get ServiceCategories
  */
export const getServiceCategories = (siteId, pageSize, pageIndex) => {
  
  return dispatch => new Promise(async (resolve, reject) => {
    await statusMessage(dispatch, 'loading', true)
    const url = `${CatalogApiUrl}/ServiceCatalog/sites/${siteId}/servicecategories?pageSize=${pageSize}&pageIndex=${pageIndex}`
    return fetch(url)
            .then(res => res.json())
            .then(async (json) => {
              await statusMessage(dispatch, 'loading', false)
              return resolve(dispatch({
                type: 'SERVICE_CATEGORIES_REPLACE',
                data: json
              }))
            })
            .catch(reject)
        // .catch(async (err) => {
        //         debugger;
        //         await statusMessage(dispatch, 'loading', false);
        //         throw err.message;
        //     })
  }).catch(async (err) => {
    await statusMessage(dispatch, 'loading', false)
    throw err.message
  })
}

export const setSelectedCategory = (item)=>{
  return dispatch => new Promise(resolve => resolve(dispatch({
    type: 'SET_SELECTED_CATEGORY',
    data: item
  })))
}