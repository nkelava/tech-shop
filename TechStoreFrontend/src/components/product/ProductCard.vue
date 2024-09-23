<script setup>
import { computed } from "vue";
import { RouterLink } from "vue-router";
import { useCartStore, useWishlistStore } from "@/store";
import CartIcon from "@/assets/icons/header/cart.png";
import FavoriteIcon from "@/assets/icons/header/favorite.png";
import SoldOutIcon from "@/assets/icons/card/sold-out-64.png";
import DefaultImage from "@/assets/images/test/products/defaultProductImage.png";

// eslint-disable-next-line no-unused-vars
const props = defineProps(["product"]);
const cart = useCartStore();
const wishlist = useWishlistStore();

function addToCart(product) {
  if (product) {
    cart.addItem(product);
  }
}

function addToWishlist(product) {
  if (product) {
    wishlist.addItem(product);
  }
}

const productPrice = computed(() => {
  const product = props?.product;
  return product?.onSale
    ? product?.price - (product?.price * product?.discount) / 100
    : product?.price;
});
</script>

<template>
  <v-hover v-slot="{ isHovering, props }">
    <v-card
      v-bind="props"
      :class="{ 'on-hover': isHovering }"
      class="card"
      width="100%"
      max-width="280"
      max-height="450"
    >
      <v-img
        class="card__image"
        :src="
          product?.imageURL
            ? product.imageURL
            : product?.imageByte
            ? `data:image/jpeg;base64,` + product?.imageByte
            : DefaultImage
        "
        height="256"
        cover
      >
        <template v-slot:placeholder>
          <div class="d-flex align-center justify-center fill-height">
            <v-progress-circular color="grey-lighten-4" indeterminate></v-progress-circular>
          </div>
        </template>

        <template v-slot:error>
          <v-img class="card__image" :src="DefaultImage" height="356" cover></v-img>
        </template>
      </v-img>

      <div class="card__bottom">
        <router-link
          class="card__link"
          :to="{
            name: 'product',
            params: {
              category: product?.subcategory?.category?.slug,
              subcategory: product?.subcategory?.slug,
              productSlug: product?.slug,
            },
          }"
        >
          <h5 class="truncate" :title="product.summary">
            {{ product.summary }}
          </h5>
        </router-link>

        <div class="card__price">
          <h5 v-if="product.onSale" class="text-decoration-line-through">${{ product.price }}</h5>
          <h3>${{ productPrice }}</h3>
        </div>

        <v-card-actions>
          <router-link
            class="card__link-btn"
            :to="{
              name: 'product',
              params: {
                category: product?.subcategory?.category?.slug,
                subcategory: product?.subcategory?.slug,
                productSlug: product?.slug,
              },
            }"
          >
            <v-btn class="card__btn">View More</v-btn>
          </router-link>
        </v-card-actions>
      </div>

      <div v-if="product.unitsInStock < 1" class="btn--sale">
        <img :src="SoldOutIcon" alt="sold out" height="40" width="40" />
      </div>

      <div class="actions">
        <v-btn
          v-if="product.unitsInStock > 0"
          :class="{ 'btn--show': isHovering }"
          variant="text"
          elevation="4"
          class="btn--hide"
          title="Add to Wishlist"
          @click="addToWishlist(product)"
        >
          <img :src="FavoriteIcon" alt="favorites" />
        </v-btn>

        <v-btn
          v-if="product.unitsInStock > 0"
          :class="{ 'btn--show': isHovering }"
          variant="text"
          elevation="4"
          class="btn--hide"
          title="Add to Cart"
          @click="addToCart(product)"
        >
          <img :src="CartIcon" alt="cart" />
        </v-btn>
      </div>
    </v-card>
  </v-hover>
</template>

<style scoped>
.card {
  position: relative;
  text-align: center;
  background-color: var(--ts-c-bg-light);
  border-radius: 10px;
  color: var(--ts-c-text-dark);
  transform: 0.3s;
}

.card:hover {
  box-shadow: 5px 5px 5px var(--ts-c-primary-dark);
  transform: scale(1.01);
}

.card__bottom {
  padding: 1rem;
}

.card__image {
  width: 100%;
  background-color: white;
  border-radius: 10px 10px 0 0;
}

.card__price {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 0.5rem;
  margin-top: 2rem;
}

.card__link {
  color: var(--ts-c-text-dark);
}

.card__link-btn {
  width: 100%;
}

.card__btn {
  width: 100%;
  font-weight: bold;
  background-color: var(--ts-c-bg-dark);
  border-radius: 5px;
  color: var(--ts-c-text-light);
  text-transform: capitalize;
}

.card__btn:hover {
  background-color: var(--ts-c-primary-dark);
}

.v-card-actions .v-btn ~ .v-btn {
  margin-inline-start: 0 !important;
}

.actions {
  position: absolute;
  display: flex;
  justify-content: space-between;
  align-items: center;
  width: 100%;
  top: 1rem;
  padding: 0 1rem;
}

.btn--sale {
  position: absolute;
  top: 1rem;
  left: 1rem;
}

.btn--hide {
  opacity: 0;
  transition: opacity 0.3s ease;
}

.btn--show {
  opacity: 1;
}

.truncate {
  display: -webkit-box;
  height: 4.5em;
  overflow: hidden;
  -webkit-line-clamp: 3;
  -webkit-box-orient: vertical;
}

@media only screen and (min-width: 60em) {
  .card {
    font-size: var(--ts-text-sm);
  }
}
</style>
