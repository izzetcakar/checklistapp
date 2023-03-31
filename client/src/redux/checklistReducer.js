import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { request } from "../server/api";
const requestUrl = "Checklist/";

export const getAllChecklists = createAsyncThunk(
  "checklists/getAll",
  async () => {
    const response = await request({
      requestUrl: requestUrl,
      apiType: "getAll",
    });
    return response.data;
  }
);
export const getChecklistById = createAsyncThunk(
  "checklists/getById",
  async (Id) => {
    const response = await request({
      requestUrl: requestUrl + `${Id}`,
      apiType: "get",
    });
    return response.data;
  }
);
export const getAllListsByOrgId = createAsyncThunk(
  "checklists/getByOrgId",
  async (Id) => {
    const response = await request({
      requestUrl: requestUrl + `GetByOrgId/${Id}`,
      apiType: "get",
    });
    return response.data;
  }
);
export const createChecklist = createAsyncThunk(
  "checklists/create",
  async (checklist, { fulfillWithValue, rejectWithValue }) => {
    const response = await request({
      requestUrl: requestUrl,
      queryData: checklist,
      apiType: "post",
    });
    if (!response.success) {
      return rejectWithValue(response.message);
    } else {
      return fulfillWithValue(null);
    }
  }
);
export const updateChecklist = createAsyncThunk(
  "checklists/update",
  async (checklist, { rejectWithValue, fulfillWithValue }) => {
    const response = await request({
      requestUrl: requestUrl,
      queryData: checklist,
      apiType: "put",
    });
    if (!response.success) {
      return rejectWithValue(response.message);
    } else {
      return fulfillWithValue(null);
    }
  }
);
export const removeChecklist = createAsyncThunk(
  "checklists/remove",
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

const checklistSlice = createSlice({
  name: "checklist",
  initialState: {
    checklists: [],
    checklist: {},
    status: "idle",
    error: null,
  },
  reducers: {
    clearChecklist: (state, action) => {
      state.checklist = {};
    },
    setChecklists: (state, action) => {
      state.checklists = action.payload;
    },
    setChecklist: (state, action) => {
      state.checklist = action.payload;
    },
  },
  extraReducers(builder) {
    builder.addCase(getAllChecklists.fulfilled, (state, action) => {
      state.checklists = action.payload;
    });
    builder.addCase(getAllListsByOrgId.fulfilled, (state, action) => {
      state.checklists = action.payload;
    });
    builder.addCase(getChecklistById.fulfilled, (state, action) => {
      state.checklist = action.payload;
    });
  },
});

export const { clearChecklist, setChecklist, setChecklists } =
  checklistSlice.actions;
export default checklistSlice.reducer;
