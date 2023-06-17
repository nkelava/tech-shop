<script setup>
import { ref } from "vue";
import SearchIcon from "@/assets/icons/header/search.png";

const search = ref("");
const emit = defineEmits(["search"]);

function handleSearch() {
  if (search.value.length > 0) {
    emit("search", search.value);
  }
}
</script>

<template>
  <div class="search-container">
    <v-text-field
      v-model="search"
      placeholder="Search..."
      variant="solo"
      density="compact"
      hide-details
      rounded
    />
    <v-btn class="search__btn" @click="handleSearch" rounded>
      <img :src="SearchIcon" alt="" />
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
  min-height: 100%;
  position: absolute;
  right: 0;
}
</style>
