import { Role } from "./roleHelper";

export function checkAuthRoleStudent(isAuthenticated, userRole) {
  if (isAuthenticated && userRole === Role.Student) {
    return true;
  } else {
    return false;
  }
}

export function checkAuthRoleAdmin(isAuthenticated, userRole) {
  if (isAuthenticated && userRole === Role.Admin) {
    return true;
  } else {
    return false;
  }
}
