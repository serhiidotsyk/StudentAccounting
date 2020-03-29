import axios from "axios";
import { logout } from "../actions/authAction";
import setAuthorizationToken from "./setAuthorizationToken";

export default function createAxiosResponseInterceptor() {
  const interceptor = axios.interceptors.response.use(
    response => {
      console.log(response);
      return response;
    },
    error => {
      // Reject promise if usual error
      console.log("logging global error", error);
      console.log(error.response);
      if (error.response && error.response.status !== 401) {
        return Promise.reject(error);
      }

      /*
       * When response code is 401, try to refresh the token.
       * Eject the interceptor so it doesn't loop in case
       * token refresh causes the 401 response
       */
      axios.interceptors.response.eject(interceptor);
      console.log(localStorage.getItem("refreshToken"));
      const refreshTokenModel = localStorage.getItem("refreshToken");
      return axios
        .post(
          "https://localhost:44358/api/auth/refreshToken?".concat(
            "refreshTokenModel=".concat(refreshTokenModel)
          )
        )
        .then(response => {
          console.log("refreshToken response", response);
          setAuthorizationToken(response.data.access_token);
          localStorage.setItem("refreshToken", response.data.refresh_token);
          localStorage.setItem("jwtToken", response.data.access_token);
          error.response.config.headers["Authorization"] =
            "Bearer " + response.data.access_token;
          return axios(error.response.config);
        })
        .catch(error => {
          logout();
          return Promise.reject(error);
        })
        .finally(createAxiosResponseInterceptor);
    }
  );
}

// // let refreshTokenPromise;

// // const createUpdateAuthInterceptor = (store, http) => async error => {
// //   const message = get(error, 'response.data.message');
// //   if (!['Token expired', 'Invalid token'].includes(message)) {
// //     return Promise.reject(error);
// //   }

// //   if (!refreshTokenPromise) {
// //     refreshTokenPromise = store.dispatch('refreshToken');
// //   }

// //   await refreshTokenPromise;
// //   refreshTokenPromise = null;

// //   return http(error.config);
// // };

// // const updateAuthCb = createUpdateAuthInterceptor(store, axios);
// // axios.interceptors.response.use(null, updateAuthCb);
