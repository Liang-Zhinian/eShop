import Store from '../Store/member'

export const initialState = Store

export default function userReducer(state = initialState, action) {
  switch (action.type) {
    case 'DONE_REFRESHING_TOKEN':{
      if (action.data) {
        console.log(action)
        return {
          ...state,
          loading: false,
          error: action.data.error,
          token: action.data.token
        }
      }
      return initialState
    }
    case 'REFRESHING_TOKEN':{
      if (action.data) {
        console.log(action)
        return {
          ...state,
          loading: true,
          error: null,
          refreshTokenPromise: action.data
        }
      }
      return initialState
    }
    // case 'AUTH': {
    //   if (action.data) {
    //     console.log(action)
    //     return {
    //       ...state,
    //       loading: false,
    //       error: null,
    //       token: action.data
    //     }
    //   }
    //   return initialState
    // }
    case 'USER_LOGIN': {
      if (action.data) {
        return {
          ...state,
          loading: false,
          error: null,
          ...action.data
        }
      }
      return initialState
    }
    case 'USER_DETAILS_UPDATE': {
      if (action.data) {
        return {
          ...state,
          loading: false,
          error: null,
          ...action.data
        }
      }
      return initialState
    }
    case 'USER_LOGIN_LOCATIONS': {
      if (action.data) {
        let currentLocation = state.currentLocation
        if (!currentLocation && action.data.siblingLocations && action.data.siblingLocations.length > 0)
          currentLocation = action.data.siblingLocations[0]
        console.log(state.currentLocation, action)
        return {
          ...state,
          loading: false,
          error: null,
          currentLocation,
          siblingLocations: action.data.siblingLocations,
        }
      }
      return initialState
    }
    case 'USER_ERROR': {
      if (action.data) {
        return {
          ...state,
          loading: false,
          error: action.data
        }
      }
      return initialState
    }
    case 'USER_RESET': {
      return initialState
    }
    default:
      return state
  }
}
