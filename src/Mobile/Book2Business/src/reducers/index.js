import status from './status';
import member from './member';
import recipes from './recipes';
import locale from './locale';
import locations from './locations';
import serviceCategories from './serviceCategories';
import serviceItems from './serviceItems';

const rehydrated = (state = false, action) => {
  switch (action.type) {
    case 'persist/REHYDRATE':
      return true;
    default:
      return state;
  }
};

export default {
  rehydrated,
  status,
  member,
  recipes,
  locale,
  locations,
  serviceCategories,
  serviceItems,
};
