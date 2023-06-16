<script setup>
import TheTopBar from "./TheTopBar.vue";
import FavoritesModal from "@/components/common/header/favorites/FavoritesModal.vue";
import CartModal from "@/components/common/header/cart/CartModal.vue";
import UserProfileDropdown from "@/components/common/header/account/UserProfileDropdown.vue";
import TheNavbar from "./TheNavbar.vue";
import SearchIcon from "@/assets/icons/header/search.png";

const isSignedIn = true;
</script>

<template>
  <div class="header">
    <!-- CONTACT INFO AND LOGIN/REGISTER -->
    <the-top-bar />
    <!-- SHOP LOGO, SEARCH BAR AND ACTION ICONS -->
    <div class="header__main">
      <div class="header__brand">
        <h1>TechShop</h1>
      </div>
      <div class="header__search">
        <input class="search__input" type="text" placeholder="Search..." />
        <button class="search__btn">
          <img :src="SearchIcon" alt="phone icon" class="search__icon" />
        </button>
      </div>
      <div class="header__dropdowns">
        <favorites-modal />
        <cart-modal />
        <user-profile-dropdown v-if="isSignedIn" />
      </div>
    </div>
    <!-- NAVIGATION LINKS -->
    <TheNavbar class="header__nav" />
  </div>
</template>

<style scoped>
.header__main,
.header__nav {
  color: var(--ts-c-text-light);
  display: flex;
  flex-wrap: wrap;
  align-items: center;
}

.header__main {
  justify-content: space-between;
  padding: 0.5rem 2rem;
}

.dropdown__icon {
  margin-inline: 0.5rem;
}

.header__search {
  position: relative;
  display: flex;
  align-items: center;
  width: 500px;
  height: 25px;
}

.search__input {
  border-radius: 30px;
  padding-left: 1rem;
  width: 500px;
  height: 100%;
  border: none;
}

.search__btn {
  background-color: var(--ts-c-bg-light);
  border: none;
  border-radius: 30px;
  width: 3rem;
  height: 25px;
  position: absolute;
  right: 0;
}

.search-btn img {
  vertical-align: middle;
}

.header__dropdowns {
  display: flex;
}

.dropdown__btn {
  border: none;
  background: transparent;
}
</style>
