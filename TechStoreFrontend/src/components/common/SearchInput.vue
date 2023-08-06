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
  position: relative;
  display: flex;
  flex: 1;
  align-items: center;
  max-width: 800px;
}

.search__btn {
  position: absolute;
  right: 5px;
  background-color: var(--ts-c-bg-light);
}

@media only screen and (max-width: 23em) {
  .search-container {
    min-width: 250px;
  }
}

@media only screen and (min-width: 23em) {
  .search-container {
    min-width: 300px;
  }
}
</style>
