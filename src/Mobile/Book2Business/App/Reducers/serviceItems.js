import Store from '../Store/serviceItems'

export const initialState = Store

export default function serviceItemReducer (state = initialState, action) {
  switch (action.type) {
    case 'SERVICE_ITEMS_ERROR': {
      return {
        ...state,
        loading: false,
        error: action.data
      }
    }
    case 'SERVICE_ITEMS_REPLACE': {
      let serviceItems = {}

      // Pick out the props I need
      if (action.data && typeof action.data === 'object') {
        serviceItems = action.data
      }

      return {
        ...state,
        error: null,
        loading: false,
        serviceItems
      }
    }
    case 'SET_SELECTED_SERVICE_ITEM': {
      return {
        ...state,
        selectedServiceItem: action.data
      }
    }
    default:
      return state
  }
}
