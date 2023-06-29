<script setup>
import { RouterLink } from "vue-router";
import CartIcon from "@/assets/icons/header/cart.png";
import FavoriteIcon from "@/assets/icons/header/favorite.png";

const props = defineProps(["product"]);
const category = "laptops";
const subcategory = "notebooks";

function addToCart(productId) {
  console.log(`Added to Cart: ${productId}`);
}

function addToWishlist(productId) {
  console.log(`Added to Wishlist: ${productId}`);
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
          @click="addToCart(product.id)"
        >
          <img :src="CartIcon" alt="cart icon" />
        </v-btn>
        <v-btn
          class="btn--absolute btn--hide"
          :class="{ 'btn--show': isHovering }"
          title="Add to Wishlist"
          @click="addToWishlist(product.id)"
        >
          <img :src="FavoriteIcon" alt="favorites icon" />
        </v-btn>
        <!-- <v-btn class="card__btn" @click="goToProductDetails">View More</v-btn> -->
        <v-btn class="card__btn">
          <router-link
            :to="{
              name: 'product',
              params: { category: category, subcategory: subcategory, productId: product.id },
            }"
            >View More</router-link
          >
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-hover>
</template>

<style scoped>
.card {
  background-color: var(--ts-c-bg-light);
  border-radius: 10px;
  color: var(--ts-c-text-dark);
  text-align: center;
  min-width: 250px;
  max-width: 300px;
  margin-inline: auto;
  padding: 10px;
}

.card__image {
  max-height: 250px;
  width: 100%;
  max-width: 250px;
  object-fit: contain;
}

.card__price {
  margin-top: 2rem;
}

.card__btn {
  background-color: var(--ts-c-bg-dark);
  border-radius: 5px;
  color: var(--ts-c-text-light);
  font-weight: bold;
  width: 100%;
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
</style>
