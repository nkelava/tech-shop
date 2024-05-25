import { axiosPublic, axiosPrivate } from "@/api/axios";
import { defineStore } from "pinia";
import { useCartStore, useWishlistStore } from "@/store";

export const useUserStore = defineStore("user", {
  state: () => ({
    user: null,
    error: null,
  }),
  getters: {
    isLoggedIn: (state) => !!state.user,
  },
  actions: {
    async getUser() {
      if (!this.isLoggedIn) return;

      // TODO
      // const response = await axiosPrivate.get(`api/v1/user/${this.user.id}`);
      // this.user = { ...response.data.user, accessToken: this.user.token };
    },

    async loginUser(email, password) {
      const cart = useCartStore();
      const wishlist = useWishlistStore();

      await axiosPublic
        .post("/auth/login", { email, password })
        .then((response) => (this.user = response.data))
        .catch((error) => (this.error = error.reponse ? error.response.data : error));

      await cart.loadData();
      await wishlist.loadData();
    },

    async registerUser(email, password, confirmPassword) {
      await axiosPublic
        .post("/auth/register", {
          email,
          password,
          confirmPassword,
          FirstName: "Test", // TODO
          LastName: "Test", // TODO
        })
        .then((response) => (this.user = response.data))
        .catch((error) => (this.error = error.reponse ? error.response.data : error));
    },

    async logoutUser() {
      const cart = useCartStore();
      const wishlist = useWishlistStore();

      await axiosPrivate
        .get("/auth/logout")
        .catch((error) => (this.error = error.reponse ? error.response.data : error));

      await cart.clearStore();
      await wishlist.clearStore();
      this.clearStore();
    },

    async deleteUser() {
      // TODO
      // await axiosPrivate.delete(`api/v1/user/${this.user.id}`);
    },

    async clearStore() {
      this.$reset();
    },
  },
  persist: true,
});
