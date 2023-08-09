<script setup>
import { RouterLink } from "vue-router";
import { useCartStore, useWishlistStore } from "@/store";
import CartIcon from "@/assets/icons/header/cart.png";
import FavoriteIcon from "@/assets/icons/header/favorite.png";

// eslint-disable-next-line no-unused-vars
const props = defineProps(["product"]);
const cart = useCartStore();
const wishlist = useWishlistStore();
const category = "laptops";
const subcategory = "notebooks";

function addToCart(product) {
  cart.addItem(product);
}

function addToWishlist(product) {
  wishlist.addItem(product);
}
</script>

<template>
  <v-hover v-slot="{ isHovering, props }">
    <v-card class="card" :class="{ 'on-hover': isHovering }" v-bind="props">
      <div class="my-4" align="center" justify="center">
        <v-img class="card__image" :src="product.img" />
      </div>
      <h4 class="truncate" :title="product.title">
        {{ product.title }}
      </h4>
      <h2 class="card__price">
        {{ product.currency + product.price }}
      </h2>
      <v-card-actions class="actions">
        <v-btn
          class="btn--absolute btn--hide"
          :class="{ 'btn--show': isHovering }"
          title="Add to Cart"
          @click="addToCart(product)"
        >
          <img :src="CartIcon" alt="cart icon" />
        </v-btn>
        <v-btn
          class="btn--absolute btn--hide"
          :class="{ 'btn--show': isHovering }"
          title="Add to Wishlist"
          @click="addToWishlist(product)"
        >
          <img :src="FavoriteIcon" alt="favorites icon" />
        </v-btn>
        <!-- <v-btn class="card__btn" @click="goToProductDetails">View More</v-btn> -->
        <router-link
          class="card__link"
          :to="{
            name: 'product',
            params: { category: category, subcategory: subcategory, productId: product.id },
          }"
        >
          <v-btn class="card__btn">View More</v-btn>
        </router-link>
      </v-card-actions>
    </v-card>
  </v-hover>
</template>

<style scoped>
.card {
  min-width: 250px;
  max-width: 300px;
  padding: 10px;
  font-size: 12px;
  text-align: center;
  background-color: var(--ts-c-bg-light);
  border-radius: 10px;
  color: var(--ts-c-text-dark);
}

.card__image {
  max-height: 200px;
  width: 100%;
  max-width: 200px;
  object-fit: contain;
}

.card__price {
  margin-top: 2rem;
}

.card__link {
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

.btn--absolute {
  position: absolute;
  bottom: 4rem;
}

.btn--hide {
  opacity: 0;
  transition: opacity 0.3s ease;
}

.btn--hide:nth-child(2) {
  right: 15px;
}

.btn--show {
  opacity: 1;
}

.truncate {
  display: block;
  overflow: hidden;
  max-height: 3.6em;
  line-height: 1.3em;
}

@media only screen and (min-width: 60em) {
  .card {
    font-size: var(--ts-text-sm);
  }

  .card__image {
    max-height: 250px;
    max-width: 250px;
  }
}
</style>
