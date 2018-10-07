import Store from '../Store/serviceCategories'

export const initialState = Store

export default function serviceCategoryReducer (state = initialState, action) {
  switch (action.type) {
    case 'SERVICE_CATEGORIES_ERROR': {
      return {
        ...state,
        loading: false,
        error: action.data
      }
    }
    case 'SERVICE_CATEGORIES_REPLACE': {
      let serviceCategories = []

      // Pick out the props I need
      if (action.data && typeof action.data === 'object') {
        serviceCategories = action.data
      }

      return {
        ...state,
        error: null,
        loading: false,
        serviceCategories
      }
    }
    case 'SET_SELECTED_CATEGORY': {
      return {
        ...state,
        selectedCategory: action.data
      }
    }
    default:
      return state
  }
}
