<script setup>
import { computed, ref } from "vue";
import { useWishlistStore } from "@/stores";
import WishlistTable from "./WishlistTable.vue";
import WishlistIcon from "@/assets/icons/header/favorite.png";

const wishlist = useWishlistStore();
const dialog = ref(false);
const pageState = ref({
  currentPage: 1,
  itemsPerPage: 3,
});

const totalPageCount = computed(() =>
  Math.ceil(wishlist.items.length / pageState.value.itemsPerPage)
);
const currentPageItems = computed(() => {
  return wishlist.items.slice(
    (pageState.value.currentPage - 1) * pageState.value.itemsPerPage,
    pageState.value.currentPage * pageState.value.itemsPerPage
  );
});

function deleteItem(productId) {
  wishlist.removeItem(productId);
}

const toggleDialog = () => {
  dialog.value = !dialog.value;
};
</script>

<template>
  <v-card variant="text">
    <v-btn class="pa-1" variant="text" @click="toggleDialog">
      <v-badge :content="wishlist.itemCount" color="var(--ts-c-primary-mute)">
        <img :src="WishlistIcon" alt="wishlist icon" />
      </v-badge>
    </v-btn>
    <v-dialog v-model="dialog" persistent width="auto">
      <v-card class="dialog">
        <v-card-title> Your Shopping Cart</v-card-title>
        <v-card-text>
          <wishlist-table :products="currentPageItems" @deleteItem="deleteItem" />
          <v-container>
            <v-row justify="center">
              <v-col cols="10">
                <v-container class="max-width">
                  <v-pagination
                    v-model="pageState.currentPage"
                    class="my-4"
                    :length="totalPageCount"
                  />
                </v-container>
              </v-col>
            </v-row>
          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-btn color="red-darken-1" variant="text" @click="toggleDialog"> Close </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-card>
</template>

<style scoped>
.dialog {
  background: var(--ts-c-bg-light);
}

.dialog * {
  color: var(--ts-c-text-dark);
}
</style>
