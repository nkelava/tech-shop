import axios from "axios";
import { defaultOptions, privateOptions } from "./axios.config";

export const axiosPublic = axios.create(defaultOptions);
export const axiosPrivate = axios.create(privateOptions);
export const axiosPrivateFile = axios.create({
  ...defaultOptions,
  headers: {
    "Content-type": "multipart/form-data",
  },
  withCredentials: true,
  credentials: "include",
});

axiosPrivate.interceptors.request.use(
  (config) => {
    const userData = JSON.parse(localStorage.getItem("user"));
    const accessToken = userData?.user?.token || null;

    if (accessToken) {
      config.headers["Authorization"] = `Bearer ${accessToken}`;
    }

    return config;
  },

  (error) => {
    return Promise.reject(error);
  }
);

axiosPrivateFile.interceptors.request.use(
  (config) => {
    const userData = JSON.parse(localStorage.getItem("user"));
    const accessToken = userData?.user?.token || null;

    if (accessToken) {
      config.headers["Authorization"] = `Bearer ${accessToken}`;
    }

    return config;
  },

  (error) => {
    return Promise.reject(error);
  }
);
