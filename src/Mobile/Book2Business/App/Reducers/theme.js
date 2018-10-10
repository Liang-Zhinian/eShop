import Store from '../Store/theme'

export const initialState = Store

export default function themeReducer (state = initialState, action) {
  switch (action.type) {
    case 'SWITCH_THEME': {
      return {
        ...state,
        name: action.data
      }
    }
    default:
      return state
  }
}
