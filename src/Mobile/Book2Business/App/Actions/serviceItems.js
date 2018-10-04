import ErrorMessages from '../Constants/errors'
import statusMessage from './status'
import site from '../Constants/site'

/**
  * Set an Error Message
  */
export function setError (message) {
  return dispatch => new Promise(resolve => resolve(dispatch({
    type: 'SERVICE_ITEMS_ERROR',
    data: message
  })))
}

/**
  * Get ServiceCategories
  */
export function getServiceItems (siteId, serviceCategoryId, pageSize, pageIndex) {
  const { api } = site

  return dispatch => new Promise(async (resolve, reject) => {
    await statusMessage(dispatch, 'loading', true)
    const url = `${api}Catalog/sites/${siteId}/servicecategories/${serviceCategoryId}/serviceitems?pageSize=${pageSize}&pageIndex=${pageIndex}`

    return fetch(url)
            .then(res => res.json())
            .then((json) => {
              return resolve(dispatch({
                type: 'SERVICE_ITEMS_REPLACE',
                data: json
              }))
            }).catch(reject)
  }).catch(async (err) => {
    await statusMessage(dispatch, 'loading', false)
    throw err.message
  })
}
