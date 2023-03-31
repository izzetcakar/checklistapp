import React from "react";
import { useSelector } from "react-redux";
import AdminPermission from "./AdminPermission";
import UserPermission from "./UserPermission";

const Permission = () => {
  const user = useSelector((state) => state.user.user);
  return <div>{user?.isAdmin ? <AdminPermission /> : <UserPermission />}</div>;
};

export default Permission;
