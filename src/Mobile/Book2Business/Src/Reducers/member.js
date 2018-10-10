import Store from '../Store/member'

export const initialState = Store

export default function userReducer(state = initialState, action) {
  switch (action.type) {
    case 'DONE_REFRESHING_TOKEN':{
      console.log(action.type)
      if (action.data) {
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
      console.log(action.type)
      if (action.data) {
        return {
          ...state,
          loading: true,
          error: null,
          refreshTokenPromise: action.data
        }
      }
      return initialState
    }
    case 'AUTH': {
      console.log(action.type)
      if (action.data) {
        return {
          ...state,
          loading: false,
          error: null,
          token: action.data
        }
      }
      return initialState
    }
    case 'USER_LOGIN': {
      console.log(action.type)
      if (action.data) {
        return {
          ...state,
          loading: false,
          error: null,
          uid: action.data.uid,
          email: action.data.email,
          emailVerified: action.data.emailVerified
        }
      }
      return initialState
    }
    case 'USER_DETAILS_UPDATE': {
      console.log(action.type)
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
        let currentLocation = null
        if (!state.currentLocation && action.data.siblingLocations && action.data.siblingLocations.length > 0)
          currentLocation = action.data.siblingLocations[0]
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
