
import statusMessage from './status'

/**
  * Switch theme
  */
export default (theme) => {
  return dispatch => new Promise(async (resolve, reject) => {
    return resolve(dispatch({
      type: 'SWITCH_THEME',
      data: theme
    }))
  }).catch(async (err) => {
    throw err.message
  })
}
