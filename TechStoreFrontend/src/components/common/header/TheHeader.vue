<script setup>
import TheTopBar from "./TheTopBar.vue";
import SearchInput from "./SearchInput.vue";
import FavoritesModal from "./favorites/FavoritesModal.vue";
import CartModal from "./cart/CartModal.vue";
import UserProfileDropdown from "./account/UserProfileDropdown.vue";
import TheNavbar from "./TheNavbar.vue";

// TODO: implement user state
const isSignedIn = true;

function handleSearch(searchText) {
  console.log("Search: ", searchText);
}
</script>

<template>
  <header>
    <!-- CONTACT INFO AND LOGIN/REGISTER -->
    <the-top-bar />
    <!-- SHOP LOGO, SEARCH BAR AND ACTION ICONS -->
    <div class="header__main">
      <h1>TechShop</h1>
      <search-input @search="handleSearch" />
      <div class="d-flex">
        <favorites-modal />
        <cart-modal />
        <user-profile-dropdown v-if="isSignedIn" />
      </div>
    </div>
    <!-- NAVIGATION LINKS -->
    <the-navbar />
  </header>
</template>

<style scoped>
.header__main {
  color: var(--ts-c-text-light);
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
  align-items: center;
  padding: 1rem 2rem;
}
</style>
