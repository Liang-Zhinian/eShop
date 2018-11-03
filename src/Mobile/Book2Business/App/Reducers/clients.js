import Store from '../Store/clients'

export const initialState = Store

export default function clientReducer (state = initialState, action) {
  switch (action.type) {
    case 'CLIENTS_ERROR': {
      return {
        ...state,
        loading: false,
        error: action.data
      }
    }
    case 'CLIENTS_FETCHING_STATUS': {
      return {
        ...state,
        error: null,
        loading: action.data,
      }
    }
    case 'CLIENTS_REPLACE': {
      let clients = {}

      // Pick out the props I need
      if (action.data && typeof action.data === 'object') {
        clients = action.data
      }

      return {
        ...state,
        error: null,
        loading: false,
        clients,
        selectedClient: null
      }
    }
    case 'SET_SELECTED_CLIENT': {
      return {
        ...state,
        selectedClient: action.data
      }
    }
    default:
      return state
  }
}
