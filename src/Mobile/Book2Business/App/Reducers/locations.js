import Store from '../Store/locations'

export const initialState = Store

export default function locationReducer (state = initialState, action) {
  switch (action.type) {
    case 'LOCATIONS_ERROR': {
      return {
        ...state,
        error: action.data
      }
    }
    case 'CURRENT_LOCATION': {
      if (action.data) {
        return {
          ...state,
          loading: false,
          error: null,
          currentLocation: action.data
        }
      }
      return initialState
    }
    case 'SIBLING_LOCATIONS': {
      if (action.data) {
        return {
          ...state,
          loading: false,
          error: null,
          siblingLocations: action.data
        }
      }
      return initialState
    }
    default:
      return state
  }
}
