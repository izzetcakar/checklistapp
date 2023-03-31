import {
  OrganizationPage,
  ProfilePage,
  SubPage,
  PermissionPage,
  BaseOptionPage,
} from "./pages";
import { withNavigationWatcher } from "./contexts/navigation";

const routes = [
  {
    path: "/profile",
    element: ProfilePage,
  },
  {
    path: "/organization",
    element: OrganizationPage,
  },
  {
    path: "/subscription",
    element: SubPage,
  },
  {
    path: "/permission",
    element: PermissionPage,
  },
  {
    path: "/baseOption",
    element: BaseOptionPage,
  },
];

export default routes.map((route) => {
  return {
    ...route,
    element: withNavigationWatcher(route.element, route.path),
  };
});
