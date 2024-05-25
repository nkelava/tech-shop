const defaultOptions = {
  baseURL: import.meta.env.VITE_API_BASE_URL,
};

const privateOptions = {
  ...defaultOptions,
  headers: {
    "Content-type": "application/json",
  },
  withCredentials: true,
  credentials: "include",
};

export { defaultOptions, privateOptions };
