import { 
  GET_ALL_USERS, 
  UPDATE_USER, 
  DELETE_USER 
} from "../actions/types";

const intitialState = {
  count:0,
  users: []
};

export default (state = intitialState, action = {}) => {
  switch (action.type) {
    case GET_ALL_USERS:
      return {
        count: action.count,
        users: action.users
      };
    case UPDATE_USER:
      return {
        users: state.users.map((item, index) => {
          if (item.id === action.user.id) {
            return {
              ...action.user
            };
          }
          return item;
        })
      };
    case DELETE_USER:
      return {
        count: state.count-1,
        users: state.users.filter(({ id }) => id !== action.user.id)
      };
    default:
      return state;
  }
};
