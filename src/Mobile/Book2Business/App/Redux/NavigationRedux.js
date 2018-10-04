import { NavigationActions } from 'react-navigation'
import { AppNavigation } from '../Navigation/AppNavigation'

const { navigate, reset } = NavigationActions
const { getStateForAction } = AppNavigation.router

const INITIAL_STATE = getStateForAction(
  navigate({ routeName: 'AuthLoading' })
)
const NOT_LOGGED_IN_STATE = getStateForAction(reset({
  index: 0,
  actions: [
    navigate({ routeName: 'Auth' })
  ]
}))
const LOGGED_IN_STATE = getStateForAction(reset({
  index: 0,
  actions: [
    navigate({ routeName: 'App' })
  ]
}))
/**
 * Creates an navigation action for dispatching to Redux.
 *
 * @param {string} routeName The name of the route to go to.
 */
// const navigateTo = routeName => () => navigate({ routeName })

export function reducer(state = INITIAL_STATE, action) {
  let nextState
  switch (action.type) {
    case 'SET_REHYDRATION_COMPLETE':
      console.log(action.type)
      return NOT_LOGGED_IN_STATE
    case 'LOGOUT':
      console.log(action.type)
      return NOT_LOGGED_IN_STATE
    case 'LOGIN_SUCCESS':
      console.log(action.type)
      return LOGGED_IN_STATE
    case 'AUTO_LOGIN':
      console.log(action.type)
      return LOGGED_IN_STATE
  }
  nextState = getStateForAction(action, state)
  return nextState || state
}
