<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";

import SearchIcon from "@/assets/icons/header/search.png";

const router = useRouter();
const search = ref("");

function handleSearch() {
  if (search.value.length > 0) {
    router.push({ path: "/search", query: { q: search.value } });
  }
}
</script>

<template>
  <div class="search-container">
    <v-text-field v-model="search" label="Search" variant="solo" density="compact" hide-details />
    <v-btn class="search__btn" @click="handleSearch">
      <img :src="SearchIcon" alt="Search" />
    </v-btn>
  </div>
</template>

<style scoped>
.search-container {
  display: flex;
  flex: 1;
  align-items: center;
  min-width: 250px;
  max-width: 800px;
  position: relative;
}

.search__btn {
  background-color: var(--ts-c-bg-light);
  position: absolute;
  right: 5px;
}
</style>
