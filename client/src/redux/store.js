import { configureStore } from "@reduxjs/toolkit";
import { getDefaultMiddleware } from "@reduxjs/toolkit";
import listItemReducer from "./listItemReducer";
import { createStore } from "redux";
import userReducer from "./userReducer";
import organizationReducer from "./organizationReducer";
import checklistReducer from "./checklistReducer";
import subscriptionReducer from "./subscriptionReducer";
import permissionReducer from "./permissionReducer";
import baseOptionReducer from "./baseOptionReducer";

const store = configureStore({
  reducer: {
    organization: organizationReducer,
    checklist: checklistReducer,
    listItem: listItemReducer,
    user: userReducer,
    subscription: subscriptionReducer,
    permission: permissionReducer,
    baseOption: baseOptionReducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: false,
    }),
});
//const store = createStore(combineReducers({ page: pageReducer }));
export default store;
