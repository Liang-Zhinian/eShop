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
    case 'STAFFS_LOADING': {
      return {
        ...state,
        loading: action.data
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
    case 'MEMBER_CHECKING_STARTED': {
      return {
        ...state,
        memberCheckingStarted: action.data
      }
    }
    case 'MEMBER_CHECKING_DONE': {
      return {
        ...state,
        memberCheckingDone: action.data
      }
    }
    case 'MEMBER_EXISTANCE': {
      return {
        ...state,
        memberExistance: action.data != null,
        memberChecked: action.data
      }
    }
    default:
      return state
  }
}
