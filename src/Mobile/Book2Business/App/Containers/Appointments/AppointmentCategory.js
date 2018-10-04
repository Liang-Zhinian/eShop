import React, { Component } from 'react'
import { TouchableOpacity, Text } from 'react-native'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { Icon } from 'native-base'
// import { Actions } from 'react-native-router-flux';

// import { getRecipes, getMeals, setError } from '../Actions/recipes';

class AppointmentCategory extends Component {
  static propTypes = {
    Layout: PropTypes.func.isRequired,
    // recipes: PropTypes.shape({
    //   loading: PropTypes.bool.isRequired,
    //   error: PropTypes.string,
    //   recipes: PropTypes.arrayOf(PropTypes.shape()).isRequired,
    // }).isRequired,
    match: PropTypes.shape({
      params: PropTypes.shape({})
    })
    // fetchRecipes: PropTypes.func.isRequired,
    // fetchMeals: PropTypes.func.isRequired,
    // showError: PropTypes.func.isRequired,
  }

  static defaultProps = {
    match: null
  }

  static renderRightButton = (props) => {
    return (
      <TouchableOpacity
        onPress={() => {
          Actions.appointment_category({ match: { params: { action: 'ADD' } } })
        }}
        style={{marginRight: 10}}>
        <Icon name='add' />
      </TouchableOpacity>
    )
  }

  componentDidMount = () => this.fetchRecipes();

  /**
    * Fetch Data from API, saving to Redux
    */
  fetchRecipes = () => {
    // const { fetchRecipes, fetchMeals, showError } = this.props;
    // return fetchRecipes()
    //   .then(() => fetchMeals())
    //   .catch((err) => {
    //     console.log(`Error: ${err}`);
    //     return showError(err);
    //   });
  }

  render = () => {
    const { Layout, /* recipes, */match } = this.props
    // const id = (match && match.params && match.params.id) ? match.params.id : null;

    return (
      <Layout
        // recipeId={id}
        // error={recipes.error}
        // loading={recipes.loading}
        // recipes={recipes.recipes}
        reFetch={() => this.fetchRecipes()}
      />
    )
  }
}

const mapStateToProps = state => ({
  // recipes: state.recipes || {},
})

const mapDispatchToProps = {
  // fetchRecipes: getRecipes,
  // fetchMeals: getMeals,
  // showError: setError,
}

export default connect(mapStateToProps, mapDispatchToProps)(AppointmentCategory)
