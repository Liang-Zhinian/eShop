import Store from '../store/locations';

export const initialState = Store;

export default function locationReducer(state = initialState, action) {
  switch (action.type) {
    case 'LOCATION': {
      if (action.data) {
          console.log(action.data);
        return {
          ...state,
          loading: false,
          error: null,
          location: action.data,
        };
      }
      return initialState;
    }
    default:
      return state;
  }
}
