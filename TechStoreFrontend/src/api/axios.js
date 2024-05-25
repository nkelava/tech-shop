import axios from "axios";
import { defaultOptions, privateOptions } from "./axios.config";

export const axiosPublic = axios.create(defaultOptions);
export const axiosPrivate = axios.create(privateOptions);

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
