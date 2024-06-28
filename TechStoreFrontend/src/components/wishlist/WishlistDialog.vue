<script setup>
import { computed, ref } from "vue";
import { useWishlistStore } from "@/store";
import WishlistTable from "./WishlistTable.vue";
import WishlistIcon from "@/assets/icons/header/favorite.png";
import EmptyStateImage from "@/assets/images/test/empty_state.webp";

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

const toggleDialog = () => {
  dialog.value = !dialog.value;
};
</script>

<template>
  <v-card variant="text">
    <v-btn class="widget__btn" variant="text" @click="toggleDialog">
      <v-badge :content="wishlist.itemCount" color="var(--ts-c-primary-mute)">
        <img :src="WishlistIcon" alt="wishlist icon" />
      </v-badge>
    </v-btn>
    <v-dialog v-model="dialog" persistent width="auto">
      <v-card class="dialog">
        <v-card-title class="font-weight-bold"> Your Wishlist </v-card-title>
        <v-card-text v-if="currentPageItems.length">
          <wishlist-table :products="currentPageItems" />
          <v-container v-if="wishlist?.items?.length > pageState.itemsPerPage">
            <v-row justify="center">
              <v-col cols="10">
                <v-container class="max-width">
                  <v-pagination
                    v-model="pageState.currentPage"
                    class="my-1"
                    :length="totalPageCount"
                  />
                </v-container>
              </v-col>
            </v-row>
          </v-container>
        </v-card-text>
        <div v-else class="empty__container">
          <v-img class="empty__image" :src="EmptyStateImage">
            <template v-slot:placeholder>
              <div class="d-flex align-center justify-center fill-height">
                <v-progress-circular
                  :size="80"
                  color="teal-darken-2"
                  indeterminate
                ></v-progress-circular>
              </div>
            </template>
          </v-img>
          <p class="text-h6">Your wishlit is empty!</p>
        </div>
        <v-card-actions>
          <v-btn color="red-darken-1" variant="text" @click="toggleDialog"> Close </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-card>
</template>

<style scoped>
.dialog {
  padding: 1rem 0.5rem;
  min-width: 400px;
  background: var(--ts-c-bg-light);
}

.dialog * {
  color: var(--ts-c-text-dark);
}

.empty__container {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 1rem 0;
}

.empty__image {
  height: 350px;
  width: 350px;
}

.empty__container p {
  color: var(--ts-c-text-dark);
}
</style>
