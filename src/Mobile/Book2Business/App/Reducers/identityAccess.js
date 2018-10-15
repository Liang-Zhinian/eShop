import Store from '../Store/identityAccess'

export const initialState = Store

export default function appointmentTypeReducer (state = initialState, action) {
  switch (action.type) {
    case 'IDENTITY_ACCESS_ERROR': {
      return {
        ...state,
        loading: false,
        error: action.data
      }
    }
    case 'IA_OFFER_REGISTRATION_INVITATION_LOADING': {
        return {
            ...state,
            loading: action.data,
            error: null
          }
    }
    case 'IA_OFFER_REGISTRATION_INVITATION_REPLACE': {
      return {
        ...state,
        registrationInvitation: action.data
      }
    }
    default:
      return state
  }
}
