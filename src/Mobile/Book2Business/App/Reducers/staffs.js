import Store from '../Store/staffs'

export const initialState = Store

export default function staffsReducer (state = initialState, action) {
  switch (action.type) {
    case 'STAFFS_ERROR': {
      return {
        ...state,
        error: action.data
      }
    }
    case 'STAFFS_REPLACE': {
      let staffs = []

      // Pick out the props I need
      if (action.data && typeof action.data === 'object') {
        staffs = action.data
      }

      return {
        ...state,
        error: null,
        loading: false,
        staffs
      }
    }
    default:
      return state
  }
}
