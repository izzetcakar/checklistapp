import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { request } from "../server/api";
const requestUrl = "BaseOption/";

export const getAllBaseOptions = createAsyncThunk(
  "baseOptions/getAll",
  async () => {
    const response = await request({
      requestUrl: requestUrl,
      apiType: "getAll",
    });
    return response.data;
  }
);
export const createBaseOption = createAsyncThunk(
  "baseOptions/create",
  async (baseOption, { fulfillWithValue, rejectWithValue }) => {
    const response = await request({
      requestUrl: requestUrl,
      queryData: baseOption,
      apiType: "post",
    });
    if (!response.success) {
      return rejectWithValue(response.message);
    } else {
      return fulfillWithValue(null);
    }
  }
);
export const updateBaseOption = createAsyncThunk(
  "baseOptions/update",
  async (baseOption, { rejectWithValue, fulfillWithValue }) => {
    const response = await request({
      requestUrl: requestUrl,
      queryData: baseOption,
      apiType: "put",
    });
    if (!response.success) {
      return rejectWithValue(response.message);
    } else {
      return fulfillWithValue(null);
    }
  }
);
export const removeBaseOption = createAsyncThunk(
  "baseOptions/remove",
  async (baseOption, { rejectWithValue, fulfillWithValue }) => {
    const response = await request({
      requestUrl: requestUrl,
      queryData: baseOption,
      apiType: "deleteByObj",
    });
    if (!response.success) {
      return rejectWithValue(response.message);
    } else {
      return fulfillWithValue(null);
    }
  }
);

const baseOptionSlice = createSlice({
  name: "baseOption",
  initialState: {
    baseOptions: {},
    categories: [],
    segments: [],
    controlLists: [],
    consepts: [],
    contents: [],
    status: "idle",
    error: null,
  },
  reducers: {
    clearBaseOption: (state, action) => {
      state.baseOptions = {};
    },
    setBaseOption: (state, action) => {
      state.baseOptions = action.payload;
    },
  },
  extraReducers(builder) {
    builder.addCase(getAllBaseOptions.fulfilled, (state, action) => {
      const { categories, segments, controlLists, consepts, contents } =
        action.payload;
      state.baseOptions = action.payload;
      state.categories = categories;
      state.segments = segments;
      state.controlLists = controlLists;
      state.consepts = consepts;
      state.contents = contents;
    });
  },
});

export const { clearBaseOption, setBaseOption } = baseOptionSlice.actions;
export default baseOptionSlice.reducer;
