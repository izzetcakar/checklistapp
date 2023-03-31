import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { request } from "../server/api";
const requestUrl = "Permission/";

export const getAllPermissions = createAsyncThunk(
  "permissions/getAll",
  async () => {
    const response = await request({
      requestUrl: requestUrl,
      apiType: "getAll",
    });
    return response.data;
  }
);
export const getPermissionsByUser = createAsyncThunk(
  "permissions/getAllByUser",
  async () => {
    const response = await request({
      requestUrl: requestUrl + "getByUser",
      apiType: "get",
    });
    return response.data;
  }
);
export const getPermissionById = createAsyncThunk(
  "permissions/getById",
  async (Id) => {
    const response = await request({
      requestUrl: requestUrl + `${Id}`,
      apiType: "get",
    });
    return response.data;
  }
);
export const createPermission = createAsyncThunk(
  "permissions/create",
  async (permission, { rejectWithValue, fulfillWithValue }) => {
    const response = await request({
      requestUrl: requestUrl,
      queryData: permission,
      apiType: "post",
    });
    if (!response.success) {
      return rejectWithValue(response.message);
    } else {
      return fulfillWithValue(null);
    }
  }
);
export const updatePermission = createAsyncThunk(
  "permissions/update",
  async (permission, { rejectWithValue, fulfillWithValue }) => {
    const response = await request({
      requestUrl: requestUrl,
      queryData: permission,
      apiType: "put",
    });
    if (!response.success) {
      return rejectWithValue(response.message);
    } else {
      return fulfillWithValue(null);
    }
  }
);
export const replyPermission = createAsyncThunk(
  "permissions/reply",
  async (permission, { rejectWithValue, fulfillWithValue }) => {
    const response = await request({
      requestUrl: requestUrl + "reply",
      queryData: permission,
      apiType: "put",
    });
    if (!response.success) {
      return rejectWithValue(response.message);
    } else {
      return fulfillWithValue(null);
    }
  }
);
export const removePermission = createAsyncThunk(
  "permissions/remove",
  async (Id, { rejectWithValue, fulfillWithValue }) => {
    const response = await request({
      requestUrl: requestUrl + `${Id}`,
      apiType: "delete",
    });
    if (!response.success) {
      return rejectWithValue(response.message);
    } else {
      return fulfillWithValue(null);
    }
  }
);

const permissionSlice = createSlice({
  name: "permission",
  initialState: {
    permissions: [],
    permission: {},
    status: "idle",
    error: null,
  },
  reducers: {
    clearPermission: (state, action) => {
      state.permission = {};
    },
    setPermissions: (state, action) => {
      state.permissions = action.payload;
    },
    setPermission: (state, action) => {
      state.permission = action.payload;
    },
  },
  extraReducers(builder) {
    builder.addCase(getAllPermissions.fulfilled, (state, action) => {
      state.permissions = action.payload;
    });
    builder.addCase(getPermissionsByUser.fulfilled, (state, action) => {
      state.permissions = action.payload;
    });
    builder.addCase(getPermissionById.fulfilled, (state, action) => {
      state.permission = action.payload;
    });
  },
});

export const { clearPermission, setPermission, setPermissions } =
  permissionSlice.actions;
export default permissionSlice.reducer;
