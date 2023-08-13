<script setup>
import { computed, ref } from "vue";

const reviews = [
  {
    name: "Ali Connors",
    rating: 4,
    review: "I'll be in your neighborhood doing errands this weekend. Do you want to hang out?",
  },
  {
    name: "Alex Scott",
    rating: 3.5,
    review:
      "Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend.",
  },
  {
    name: "Sandra Adams",
    rating: 5,
    review: "Do you have Paris recommendations? Have you ever been?",
  },

  {
    name: "Ali Connorsa",
    rating: 4,
    review: "I'll be in your neighborhood doing errands this weekend. Do you want to hang out?",
  },
  {
    name: "Alex Scott",
    rating: 3.5,
    review:
      "Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend. Wish I could come, but I'm out of town this weekend.",
  },
  {
    name: "Sandra Adams",
    rating: 5,
    review: "Do you have Paris recommendations? Have you ever been?",
  },
];
const pageState = ref({
  currentPage: 1,
  itemsPerPage: 5,
});
const totalPageCount = computed(() => Math.ceil(reviews.length / pageState.value.itemsPerPage));

const currentPageItems = computed(() => {
  return reviews.slice(
    (pageState.value.currentPage - 1) * pageState.value.itemsPerPage,
    pageState.value.currentPage * pageState.value.itemsPerPage
  );
});
</script>

<template>
  <v-card>
    <v-list lines="10">
      <v-list-item v-for="(review, i) in currentPageItems" :key="i">
        <div class="review">
          <span class="review__name">{{ review.name }}</span>
          <div class="review__info">
            <v-rating v-model="review.rating" size="small" density="compact" readonly> </v-rating>
            <p>{{ new Date().toJSON().slice(0, 10).replace(/-/g, "/") }}</p>
          </div>
          <span class="review__text">{{ review.review }}</span>
        </div>
        <v-divider inset></v-divider>
      </v-list-item>
      <v-container>
        <v-row justify="center">
          <v-col cols="10">
            <v-container class="max-width">
              <v-pagination v-model="pageState.currentPage" class="my-4" :length="totalPageCount" />
            </v-container>
          </v-col>
        </v-row>
      </v-container>
    </v-list>
  </v-card>
</template>

<style scoped>
.v-list {
  background-color: var(--ts-c-bg-light);
  color: var(--ts-c-text-dark);
}
.v-list-item {
  margin: auto;
}

.v-list-item hr {
  margin: 20px auto;
}

.review {
  display: flex;
  flex-direction: column;
}

.review__name {
  font-weight: bold;
}

.review__info {
  display: flex;
  align-items: center;
  gap: 5px;
}

.review__text {
  margin-top: 10px;
}
</style>
