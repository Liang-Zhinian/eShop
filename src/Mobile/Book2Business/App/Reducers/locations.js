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
    case 'LOCATION': {
      if (action.data) {
        return {
          ...state,
          loading: false,
          error: null,
          location: action.data
        }
      }
      return initialState
    }
    default:
      return state
  }
}
