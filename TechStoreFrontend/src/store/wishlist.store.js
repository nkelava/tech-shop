import { defineStore } from "pinia";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import { useUserStore } from "@/store";

const toast = useToast();

export const useWishlistStore = defineStore("wishlist", {
  state: () => ({
    items: [],
  }),
  getters: {
    itemCount: (state) => state.items.length,
    isUserLoggedIn: () => {
      const user = useUserStore();
      return user.isLoggedIn;
    },
  },
  actions: {
    async addItem(item) {
      const itemExists = this.isProductAlreadyInWishlist(item?.id);

      if (itemExists >= 0) {
        toast.info("This product is already in your wishlist.");
        return;
      }

      try {
        if (this.isUserLoggedIn) {
          const resp = await axiosPrivate
            .post("/wishlists", {
              productId: item?.id,
            })
            .catch((error) => {
              toast.error(
                "Oops! Something went wrong while adding the product to your wishlist. Please try again later or contact support for assistance."
              );
              console.log(error);
            });

          if (!resp?.data) return;
        }

        this.items.push(item);
        this.persistData();
        toast.success("Success! The product has been added to your wishlist.");
      } catch {
        toast.error(
          "Oops! Something went wrong while adding the product to your wishlist. Please try again later or contact support for assistance."
        );
      }
    },

    async removeItem(productId) {
      try {
        if (this.isUserLoggedIn) {
          const resp = await axiosPrivate.delete(`/wishlists/${productId}`).catch((error) => {
            toast.error(
              "Oops! Something went wrong while trying to remove the item from your wishlist. Please try again later or contact customer support for assistance."
            );
            console.log(error);
          });

          if (!resp?.data) {
            toast.error(
              "Oops! Something went wrong while trying to remove the item from your wishlist. Please try again later or contact customer support for assistance."
            );
            return;
          }
        }

        this.items = this.items.filter((item) => item.id != productId);
        this.persistData();
      } catch {
        toast.error(
          "Oops! Something went wrong while trying to remove the item from your wishlist. Please try again later or contact customer support for assistance."
        );
      }
    },

    isProductAlreadyInWishlist(productId) {
      if (this.items.length < 1) return -1;

      return this.items.findIndex((item) => item.id === productId);
    },

    persistData() {
      localStorage.setItem("wishlist", JSON.stringify(this.items));
    },

    async loadData() {
      try {
        const data =
          this.isUserLoggedIn &&
          (await axiosPrivate
            .get("/wishlists")
            .then((resp) => {
              let formattedWishlist = [];

              if (resp?.data?.products) {
                formattedWishlist = resp?.data?.products.map((product) => {
                  return { ...product.product };
                });

                return JSON.stringify(formattedWishlist);
              }

              return [];
            })
            .catch((error) => console.log(error)));

        if (data) {
          this.items = JSON.parse(data);
          this.persistData();
        }
      } catch {
        console.log("There was an error loading wishlist data...");
      }
    },

    async clearStore() {
      this.$reset();
    },
  },
  persist: true,
});
