import { axiosPublic, axiosPrivate } from "@/api/axios";
import { defineStore } from "pinia";
import { useCartStore, useWishlistStore } from "@/store";

export const useUserStore = defineStore("user", {
  state: () => ({
    user: null,
  }),
  getters: {
    isLoggedIn: (state) => !!state.user,
  },
  actions: {
    async loginUser(email, password) {
      try {
        const { data } = await axiosPublic.post("/auth/login", { email, password });
        this.user = data;

        const cart = useCartStore();
        const wishlist = useWishlistStore();
        await Promise.all([cart.loadData(), wishlist.loadData()]);
      } catch (error) {
        throw error;
      }
    },

    async registerUser(firstName, lastName, email, password, confirmPassword) {
      try {
        await axiosPublic.post("/auth/register", {
          firstName,
          lastName,
          email,
          password,
          confirmPassword,
        });
      } catch (error) {
        this.error = error.response ? error.response.data : error;
        throw error; // Propagate the error
      }
    },

    async logoutUser() {
      const cart = useCartStore();
      const wishlist = useWishlistStore();

      await axiosPrivate.get("/auth/logout").catch((error) => {
        console.log(error);
      });

      await cart.clearStore();
      await wishlist.clearStore();
      this.clearStore();
    },

    async clearStore() {
      this.$reset();
    },
  },
  persist: true,
});
