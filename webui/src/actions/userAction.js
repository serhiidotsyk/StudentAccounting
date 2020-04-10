import axios from "axios";
import { ADD_USER } from "./types";

import { API_ADD_USER } from "../config";

import { trackPromise } from "react-promise-tracker";

export function createUser(user) {
  return {
    type: ADD_USER,
    user,
  };
}

export function addUser(user) {
  return (dispatch) => {
    return trackPromise(
      axios.post(API_ADD_USER, user).then((res) => {
        dispatch(createUser(res.data));
      })
    );
  };
}
