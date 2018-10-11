import Store from '../Store/nearby'

export const initialState = Store

export default function nearbyReducer (state = initialState, action) {
  switch (action.type) {
    case 'NEARBY_ERROR': {
      return {
        ...state,
        error: action.data
      }
    }
    case 'NEARBY': {
      if (action.data) {
        return {
          ...state,
          loading: false,
          error: null,
          nearby: action.data
        }
      }
      return initialState
    }
    default:
      return state
  }
}
