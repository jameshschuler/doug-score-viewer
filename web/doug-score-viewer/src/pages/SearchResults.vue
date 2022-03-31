<template>
  <div>
    <div class="columns">
      <div class="column is-10 is-offset-1 p-3">
        <SearchResultsHeader />
        <SearchResultsContainer />
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import SearchResultsContainer from "../components/Search/SearchResultsContainer.vue";
import SearchResultsHeader from "../components/Search/SearchResultsHeader.vue";
</script>
<style lang="scss" scoped></style>
