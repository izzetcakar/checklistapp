import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { request } from "../server/api";
const requestUrl = "Organization/";

export const getOrganizationsByUser = createAsyncThunk(
  "organizations/getAllByUser",
  async () => {
    const response = await request({
      requestUrl: requestUrl + "getByUser",
      apiType: "get",
    });
    return response.data;
  }
);
export const getAllOrganizations = createAsyncThunk(
  "organizations/getAll",
  async () => {
    const response = await request({
      requestUrl: requestUrl,
      apiType: "getAll",
    });
    return response.data;
  }
);
export const getOrganizationById = createAsyncThunk(
  "organizations/getById",
  async (Id) => {
    const response = await request({
      requestUrl: requestUrl + `${Id}`,
      apiType: "get",
    });
    return response.data;
  }
);
export const createOrganization = createAsyncThunk(
  "organizations/create",
  async (organization, { rejectWithValue, fulfillWithValue }) => {
    const response = await request({
      requestUrl: requestUrl,
      queryData: organization,
      apiType: "post",
    });
    if (!response.success) {
      return rejectWithValue(response.message);
    } else {
      return fulfillWithValue(null);
    }
  }
);
export const updateOrganization = createAsyncThunk(
  "organizations/update",
  async (organization, { rejectWithValue, fulfillWithValue }) => {
    const response = await request({
      requestUrl: requestUrl,
      queryData: organization,
      apiType: "put",
    });
    if (!response.success) {
      return rejectWithValue(response.message);
    } else {
      return fulfillWithValue(null);
    }
  }
);
export const removeOrganization = createAsyncThunk(
  "organizations/remove",
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

const organizationSlice = createSlice({
  name: "organization",
  initialState: {
    organizations: [],
    organization: {},
    status: "idle",
    error: null,
  },
  reducers: {
    clearOrganization: (state, action) => {
      state.organization = {};
    },
    setOrganizations: (state, action) => {
      state.organizations = action.payload;
    },
    setOrganization: (state, action) => {
      state.organization = action.payload;
    },
  },
  extraReducers(builder) {
    builder.addCase(getAllOrganizations.fulfilled, (state, action) => {
      state.organizations = action.payload;
    });
    builder.addCase(getOrganizationsByUser.fulfilled, (state, action) => {
      state.organizations = action.payload;
    });
    builder.addCase(getOrganizationById.fulfilled, (state, action) => {
      state.organization = action.payload;
    });
  },
});

export const { clearOrganization, setOrganization, setOrganizations } =
  organizationSlice.actions;
export default organizationSlice.reducer;
