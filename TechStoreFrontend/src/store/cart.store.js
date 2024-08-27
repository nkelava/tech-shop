import { defineStore } from "pinia";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import { useUserStore } from "@/store";

const toast = useToast();

export const useCartStore = defineStore("cart", {
  state: () => ({
    items: [],
  }),
  getters: {
    totalPrice: (state) => (promoCodeDiscount) => {
      const sum = state.items.reduce(
        (total, product) => (total += product.price * product.quantity),
        0
      );
      const discount = sum * (promoCodeDiscount / 100);
      return (sum - discount).toFixed(2);
    },
    itemCount: (state) => state.items.length,
    productQuantity: (state) => (id) => {
      const itemIndex = state.items.findIndex((item) => item.id === id);

      if (itemIndex >= 0) {
        return state.items[itemIndex].quantity;
      }
    },
    productTotal: (state) => (id) => {
      const itemIndex = state.items.findIndex((item) => item.id === id);

      if (itemIndex >= 0) {
        return (state.items[itemIndex].quantity * state.items[itemIndex].price).toFixed(2);
      }
    },
    isUserLoggedIn: () => {
      const user = useUserStore();
      return user.isLoggedIn;
    },
    formattedCartItemsForOrder: (state) => {
      return state.items.map((item) => {
        return {
          quantity: item.quantity,
          unitPrice: item.price,
          totalPrice: item.quantity * item.price,
          product: { ...item },
        };
      });
    },
  },
  actions: {
    async addItem(item, quantity = 1) {
      const itemExists = this.isProductAlreadyInCart(item?.id);

      if (itemExists >= 0) {
        toast.info("This product is already in your cart.");
        return;
      }

      try {
        if (this.isUserLoggedIn) {
          const resp = await axiosPrivate
            .post("/carts", {
              productId: item?.id,
              quantity,
            })
            .catch((error) => {
              toast.error(
                "Oops! Something went wrong while adding the product to your cart. Please try again later or contact support for assistance."
              );
              console.log(error);
            });

          if (!resp?.data) return;
        }

        item = { ...item, quantity };
        this.items.push(item);
        this.persistData();
        toast.success("Success! The product has been added to your cart. Happy shopping!");
      } catch {
        toast.error(
          "Oops! Something went wrong while adding the product to your cart. Please try again later or contact support for assistance."
        );
      }
    },

    async removeItem(productId) {
      try {
        if (this.isUserLoggedIn) {
          const resp = await axiosPrivate.delete(`/carts/${productId}`).catch((error) => {
            toast.error(
              "Oops! Something went wrong while trying to remove the item from your cart. Please try again later or contact customer support for assistance."
            );
            console.log(error);
          });

          if (!resp?.data) {
            toast.error(
              "Oops! Something went wrong while trying to remove the item from your cart. Please try again later or contact customer support for assistance."
            );
            return;
          }
        }

        this.items = this.items.filter((item) => item.id != productId);
        this.persistData();
      } catch {
        toast.error(
          "Oops! Something went wrong while trying to remove the item from your cart. Please try again later or contact customer support for assistance."
        );
      }
    },

    async incrementQuantity(productId) {
      const itemIndex = this.isProductAlreadyInCart(productId);

      if (itemIndex < 0) return;

      try {
        if (this.isUserLoggedIn) {
          const resp = await axiosPrivate
            .post("/carts", {
              productId,
              quantity: this.items[itemIndex].quantity + 1,
            })
            .catch((error) => {
              toast.error(
                "Oops! Something went wrong while incrementing product quantity. Please try again later or contact support for assistance."
              );
              console.log(error);
            });

          if (!resp?.data) return;
        }

        this.items[itemIndex].quantity += 1;
        this.persistData();
      } catch {
        toast.error(
          "Oops! Something went wrong while incrementing product quantity. Please try again later or contact support for assistance."
        );
      }
    },

    async decrementQuantity(productId) {
      const itemIndex = this.isProductAlreadyInCart(productId);

      if (itemIndex < 0) return;

      if (this.items[itemIndex].quantity > 1) {
        try {
          if (this.isUserLoggedIn) {
            const resp = await axiosPrivate
              .post("/carts", {
                productId,
                quantity: this.items[itemIndex].quantity - 1,
              })
              .catch((error) => {
                toast.error(
                  "Oops! Something went wrong while decrementing product quantity. Please try again later or contact support for assistance."
                );
                console.log(error);
              });

            if (!resp?.data) return;
          }

          this.items[itemIndex].quantity -= 1;
          this.persistData();
        } catch {
          toast.error(
            "Oops! Something went wrong while decrementing product quantity. Please try again later or contact support for assistance."
          );
        }
      }
    },

    isProductAlreadyInCart(productId) {
      if (this.items.length < 1) return -1;

      return this.items.findIndex((item) => item.id === productId);
    },

    persistData() {
      localStorage.setItem("cart", JSON.stringify(this.items));
    },

    async loadData() {
      try {
        const data =
          this.isUserLoggedIn &&
          (await axiosPrivate
            .get("/carts")
            .then((resp) => {
              let formattedCart = [];

              if (resp?.data?.products) {
                formattedCart = resp?.data?.products.map((product) => {
                  return { ...product.product, quantity: product?.quantity };
                });

                return JSON.stringify(formattedCart);
              }

              return [];
            })
            .catch((error) => console.log(error)));

        if (data) {
          this.items = JSON.parse(data);
          this.persistData();
        }
      } catch {
        console.log("There was an error loading cart data...");
      }
    },

    async clearStore() {
      this.$reset();

      if (this.isUserLoggedIn) {
        const resp = await axiosPrivate.delete("/carts").catch((error) => console.log(error));

        if (resp.status !== 200) {
          toast.error("Uh-oh! There was an issue while cleaning your cart. Please try again.");
          return;
        }
      }
    },
  },
  persist: true,
});
