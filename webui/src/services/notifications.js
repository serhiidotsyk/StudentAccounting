import { notification } from "antd";

export const openNotification = {
  error: (title, errors) => {
    notification.error({
      message: `${title}`,
    });
  },
  success: (title, errors) => {
    notification.success({
      message: `${title}`,
    });
  },
};
