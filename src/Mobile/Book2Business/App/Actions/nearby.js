import ErrorMessages from '../Constants/errors'
import statusMessage from './status'
import { translate, getNearby } from '../Services/TencentMapService'

/**
  * Set an Error Message
  */
export function setError (message) {
  return dispatch => new Promise(resolve => resolve(dispatch({
    type: 'NEARBY_ERROR',
    data: message
  })))
}

export function fetchNearby (latlng, pageSize = 20, pageIndex = 1) {
  return dispatch => new Promise(async (resolve, reject) => {
    await statusMessage(dispatch, 'loading', true)

    latlng = await translate(latlng).catch(reject)

    const nearby = await getNearby(latlng, pageSize, pageIndex).catch(reject)

    await statusMessage(dispatch, 'loading', false)
    return resolve(dispatch({
      type: 'NEARBY',
      data: {
        pageIndex,
        pageSize,
        count: nearby.length,
        data: nearby
      }
    }))
  }).catch(async (err) => {
    await statusMessage(dispatch, 'loading', false)
    throw err.message
  })
}
