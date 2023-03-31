import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { request } from "../server/api";
const requestUrl = "Subscription/";

export const getAllSubscriptions = createAsyncThunk(
  "subscriptions/getAll",
  async () => {
    const response = await request({
      requestUrl: requestUrl,
      apiType: "getAll",
    });
    return response.data;
  }
);
export const getSubscriptionsByUser = createAsyncThunk(
  "subscriptions/getAllByUser",
  async () => {
    const response = await request({
      requestUrl: requestUrl + `getByUser`,
      apiType: "get",
    });
    return response.data;
  }
);
export const getOrgsToAddByUserId = createAsyncThunk(
  "subscriptions/getOrgsToAddByUserId",
  async (Id) => {
    const response = await request({
      requestUrl: requestUrl + `getOrgsByUserId/${Id}`,
      apiType: "get",
    });
    return response.data;
  }
);
export const getSubscriptionById = createAsyncThunk(
  "subscriptions/getById",
  async (Id) => {
    const response = await request({
      requestUrl: requestUrl + `${Id}`,
      apiType: "get",
    });
    return response.data;
  }
);
export const createSubscription = createAsyncThunk(
  "subscriptions/create",
  async (subscription) => {
    await request({
      requestUrl: requestUrl,
      queryData: subscription,
      apiType: "post",
    });
  }
);
export const updateSubscription = createAsyncThunk(
  "subscriptions/update",
  async (subscription) => {
    await request({
      requestUrl: requestUrl,
      queryData: subscription,
      apiType: "put",
    });
  }
);
export const removeSubscription = createAsyncThunk(
  "subscriptions/remove",
  async (Id) => {
    await request({
      requestUrl: requestUrl + `${Id}`,
      apiType: "delete",
    });
  }
);
export const removeSubByUserId = createAsyncThunk(
  "subscriptions/removeByUserId",
  async (Id) => {
    await request({
      requestUrl: requestUrl + `removeByUserId/${Id}`,
      apiType: "delete",
    });
  }
);

const subscriptionSlice = createSlice({
  name: "subscription",
  initialState: {
    subscriptions: [],
    userSubs: [],
    subscription: {},
    orgsToAdd: [],
    status: "idle",
    error: null,
  },
  reducers: {
    clearSubscription: (state, action) => {
      state.subscription = {};
    },
    clearSubscriptions: (state, action) => {
      state.subscriptions = {};
    },
    setSubscriptions: (state, action) => {
      state.subscriptions = action.payload;
    },
    setSubscription: (state, action) => {
      state.subscription = action.payload;
    },
  },
  extraReducers(builder) {
    builder.addCase(getAllSubscriptions.fulfilled, (state, action) => {
      state.subscriptions = action.payload;
    });
    builder.addCase(getSubscriptionById.fulfilled, (state, action) => {
      state.subscription = action.payload;
    });
    builder.addCase(getOrgsToAddByUserId.fulfilled, (state, action) => {
      state.orgsToAdd = action.payload;
    });
    builder.addCase(getSubscriptionsByUser.fulfilled, (state, action) => {
      state.userSubs = action.payload;
    });
  },
});

export const {
  clearSubscription,
  clearSubscriptions,
  setSubscription,
  setSubscriptions,
} = subscriptionSlice.actions;
export default subscriptionSlice.reducer;
