import { openNotification } from "./notifications";

function errorResponseHandler(error) {
  // check for errorHandle config
  if (
    error.config.hasOwnProperty("errorHandle") &&
    error.config.errorHandle === false
  ) {
    return Promise.reject(error);
  }

  // if has response show the error
  if (error.response) {
    console.log(error.response.data.title);
    openNotification.error(error.response.data.title);
    return Promise.reject(error);
  }
}

export default errorResponseHandler;
