import React, { Component } from 'react';
import { TouchableOpacity, Text } from 'react-native';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { Icon } from 'native-base';
import { Actions } from 'react-native-router-flux';

import site from '../../constants/site';
import { getServiceCategories, setError } from '../../actions/serviceCategories';

class ServiceCategoryListing extends Component {
  static propTypes = {
    Layout: PropTypes.func.isRequired,
    serviceCategories: PropTypes.shape({
      loading: PropTypes.bool.isRequired,
      error: PropTypes.string,
      serviceCategories: PropTypes.arrayOf(PropTypes.shape()).isRequired,
    }).isRequired,
    match: PropTypes.shape({
      params: PropTypes.shape({}),
    }),
    fetchServiceCategories: PropTypes.func.isRequired,
    // fetchMeals: PropTypes.func.isRequired,
    showError: PropTypes.func.isRequired,
  }

  static defaultProps = {
    match: null,
  }

  static renderRightButton = (props) => {
    return (
      <TouchableOpacity
        onPress={() => { Actions.appointment_category({ match: { params: { action: 'ADD' } } }) }}
        style={{marginRight: 10}}>
        <Icon name='add' />
      </TouchableOpacity>
    );
  }

  componentDidMount = () => this.fetchServiceCategories();

  /**
    * Fetch Data from API, saving to Redux
    */
  fetchServiceCategories = () => {
    const { fetchServiceCategories, showError } = this.props;
    return fetchServiceCategories(site.siteId, 10, 0)
    //   .then(() => fetchMeals())
      .catch((err) => {
        console.log(`Error: ${err}`);
        return showError(err);
      });
  }

  render = () => {
    const { Layout, serviceCategories, match } = this.props;
    const id = (match && match.params && match.params.id) ? match.params.id : null;


    debugger;
    let listViewData = [];
    if (serviceCategories.serviceCategories && serviceCategories.serviceCategories.Data){
      listViewData = serviceCategories.serviceCategories.Data.map(item=>({
          id: item.Id,
          title: item.Name,
          content: item.Description
      }));
    }
    console.log(listViewData);

    return (
      <Layout
        serviceCategoryId={id}
        error={serviceCategories.error}
        loading={serviceCategories.loading}
        serviceCategories={listViewData}
        reFetch={() => this.fetchServiceCategories()}
      />
    );
  }
}

const mapStateToProps = state => ({
    serviceCategories: state.serviceCategories || {},
});

const mapDispatchToProps = {
  fetchServiceCategories: getServiceCategories,
  showError: setError,
};

export default connect(mapStateToProps, mapDispatchToProps)(ServiceCategoryListing);
