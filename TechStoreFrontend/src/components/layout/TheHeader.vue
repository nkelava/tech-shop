<script setup>
import { RouterLink } from "vue-router";
import SearchInput from "@/components/common/SearchInput.vue";
import WishlistDialog from "@/components/wishlist/WishlistDialog.vue";
import CartDialog from "@/components/cart/CartDialog.vue";
import UserProfileDropdown from "@/components/UserProfileDropdown.vue";
import BrandLogo from "@/assets/icons/header/brand-logo.svg";

// TODO: implement user state
const isSignedIn = true;
</script>

<template>
  <header>
    <div class="header__main">
      <h1>
        <router-link to="/">
          <img class="brand-logo" :src="BrandLogo" alt="tech planet logo" />
        </router-link>
      </h1>
      <search-input />
      <div class="d-flex">
        <wishlist-dialog />
        <cart-dialog />
        <user-profile-dropdown v-if="isSignedIn" />
      </div>
    </div>
  </header>
</template>

<style scoped>
.header__main {
  color: var(--ts-c-text-light);
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
  align-items: center;
  padding: 0.5rem 2rem;
}

.brand-logo {
  width: 100%;
  height: 100%;
}
</style>
